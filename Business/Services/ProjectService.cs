﻿using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, UserManager<MemberEntity> userManager) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<bool> CreateProjectAsync(ProjectRegForm formData, List<string> memberIds)
    {
        try
        {
            if (formData == null) 
                return false;

            var duplicate = await _projectRepository.GetOneAsync(x => x.ProjectName == formData.ProjectName);
            if (duplicate != null) 
                return false;

            var members = await GetMembersByIdsAsync(memberIds);

            var entity = ProjectFactory.Create(formData, members);
            return await _projectRepository.CreateAsync(entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not create the project || {ex.Message}");
            return false;
        }
    }
    private async Task<List<MemberEntity>> GetMembersByIdsAsync(List<string> memberIds)
    {
        // got help with this one, had trouble with the syntax
        // this is the method that gets the members by their ids
        // from the forms input and adds them to a list.
        var members = new List<MemberEntity>();
        foreach (var memberId in memberIds)
        {
            var member = await _userManager.FindByIdAsync(memberId);
            if (member != null)
            {
                members.Add(member);
            }
        }
        return members;
    }

    public async Task<Project> GetOneProjectIncludeAllAsync(int Id)
    {
        try
        {
            var entity = await _projectRepository.GetOneIncludeAllAsync(Id);
            if (entity == null)
                return null!;

            var project = ProjectFactory.Create(entity);

            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the project || {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        try
        {
            var entities = await _projectRepository.GetAllIncludeAllAsync();
            if (entities == null)
                return [];

            var projects = entities.Select(ProjectFactory.Create);
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the projects || {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<Project>> GetCompletedProjects()
    {
        try
        {
            var entities = await _projectRepository.GetAllIncludeAllAsync(p => p.IsCompleted == true);
            if (entities == null)
                return [];

            var projects = entities.Select(ProjectFactory.Create);
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the projects || {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<Project>> GetOngoingProjects()
    {
        try
        {
            var entities = await _projectRepository.GetAllIncludeAllAsync(p => p.IsCompleted == false);
            if (entities == null)
                return [];

            var projects = entities.Select(ProjectFactory.Create);
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the projects || {ex.Message}");
            return [];
        }
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdForm formData, List<string> memberIds)
    {
        try
        {
            if (formData == null) return false;

            var entity = await _projectRepository.GetOneIncludeAllAsync(id);
            if (entity == null) return false;


            var newMembers = await GetMembersByIdsAsync(memberIds);

            ProjectFactory.Update(entity, formData, newMembers);

            var result = await _projectRepository.UpdateAsync(x => x.Id == id, entity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not update the project || {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
            var entity = await _projectRepository.GetOneIncludeAllAsync(id);
            if (entity == null)
                return false;

            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not delete the project || {ex.Message}");
            return false;
        }
    }
}
