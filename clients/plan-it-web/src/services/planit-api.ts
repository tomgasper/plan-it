import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

import type { Project, ProjectTask, Workspace, WorkspaceProjects } from '../types/Project'
import { deleteWorkspace, updateWorkspace } from '../redux/workspacesSlice';

const HOST = "https://localhost:5234";
const TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMGQ5MTMwMy1iNWM5LTQ1MzAtOTkxNC1kMjdjN2EwNTQ0MTUiLCJnaXZlbl9uYW1lIjoiSmFjdcWbIiwiZmFtaWx5X25hbWUiOiJCb3NhayIsImp0aSI6IjMzMjFlYTgyLTU5ZGUtNDczMS05MDMzLTllM2M4YTAwNDhkMSIsImV4cCI6MTcyNjA2NTEzNywiaXNzIjoiUGxhbkl0IiwiYXVkIjoiUGxhbkl0In0.l5Q00XDMDhCw4EQcSHMoUj-Pat7QAMhDFuy9lHWiU0g";

// Define a service using a base URL and expected endpoints
export const projectApi = createApi({
  reducerPath: 'projectApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${HOST}/api`,
    mode: 'cors',
    headers: { Authorization: `Bearer ${TOKEN}`}
}),
  endpoints: (builder) => ({
    getProject: builder.query<Project, string>({
      query: (id) => `projects/${id}`,
    }),
    getUserWorkspaces: builder.query<Workspace[], string>({
      query: (userId) => `users/${userId}/workspaces/`,
  }),
    getProjectsForWorkspace: builder.query<WorkspaceProjects, string>({
    query: (workspaceId) => `workspaces/${workspaceId}/projects`,
    }),
    createProject: builder.mutation<Project, Partial<Project>>({
      query: (newProject) => ({
        url: `projects/`,
        method: 'POST',
        body: newProject,
      }),
    }),
    updateProject: builder.mutation<Project, { updatedProject: Partial<Project>, projectId: string }>({
      query: ({ updatedProject, projectId }) => ({
        url: `projects/${projectId}`,
        method: 'PUT',
        body: updatedProject,
      })}),
    deleteProject: builder.mutation<void, string>({
      query: (projectId) => ({
        url: `projects/${projectId}`,
        method: 'DELETE',
      }),
    }),
    getWorkspace: builder.query<Workspace, string>({
      query: (workspaceId) => `workspaces/${workspaceId}`,
    }),
    createWorkspace: builder.mutation<Workspace, Partial<Workspace>>({
      query: (newWorkspace) => ({
        url: `workspaces/`,
        method: 'POST',
        body: newWorkspace,
      }),
    }),
    updateWorkspace: builder.mutation<Workspace, { updatedWorkspace: Partial<Workspace>, workspaceId: string }>({
      query: ({ updatedWorkspace, workspaceId }) => ({
        url: `workspaces/${workspaceId}`,
        method: 'PUT',
        body: updatedWorkspace,
      })}),
    deleteWorkspace: builder.mutation<void, string>({
      query: (workspaceId) => ({
        url: `workspaces/${workspaceId}`,
        method: 'DELETE',
      }),
    }),
    // Project tasks
    getProjectTasks: builder.query<ProjectTask, string>({
      query: (projectId) => `projects/${projectId}`,
    }),
    createProjectTask: builder.mutation<void, { projectId: string, task: Partial<ProjectTask> }>({
      query: ({ projectId, task }) => ({
        url: `projects/${projectId}/tasks`,
        method: 'POST',
        body: task,
      })
    }),
    updateProjectTask: builder.mutation<void, { projectId: string, taskId: string, updatedTask: Partial<ProjectTask> }>({
      query: ({ projectId, taskId, updatedTask }) => ({
        url: `projects/${projectId}/tasks/${taskId}`,
        method: 'PUT',
        body: updatedTask,
      })}),
      deleteProjectTask: builder.mutation<void, { projectId: string, taskId: string }>({
        query: ({ projectId, taskId }) => ({
          url: `projects/${projectId}/tasks/${taskId}`,
          method: 'DELETE',
    })})
})});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useGetProjectQuery,
  useGetUserWorkspacesQuery,
  useGetProjectsForWorkspaceQuery,
  useCreateProjectMutation,
  useUpdateProjectMutation,
  useDeleteProjectMutation,
  useGetWorkspaceQuery,
  useCreateWorkspaceMutation,
  useUpdateWorkspaceMutation,
  useDeleteWorkspaceMutation,
  useCreateProjectTaskMutation,
  useDeleteProjectTaskMutation,
  useUpdateProjectTaskMutation,
  useGetProjectTasksQuery
 } = projectApi;