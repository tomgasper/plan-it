import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

import type { Project, Workspace, WorkspaceProjects } from '../types/Project'
import { deleteWorkspace } from '../redux/workspacesSlice';

const HOST = "https://localhost:5234";
const TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMGQ5MTMwMy1iNWM5LTQ1MzAtOTkxNC1kMjdjN2EwNTQ0MTUiLCJnaXZlbl9uYW1lIjoiSmFjdcWbIiwiZmFtaWx5X25hbWUiOiJCb3NhayIsImp0aSI6IjY5Yzg5OGVmLWQ2ODctNDdhOS1hNzEzLTgwZmZmODY5MmI1MyIsImV4cCI6MTcyNTk3MTgyNiwiaXNzIjoiUGxhbkl0IiwiYXVkIjoiUGxhbkl0In0.zBy2yXBVdu9-3RQeFTjFJNCdb0M_6vTAF_N6LNSINEs";

// Define a service using a base URL and expected endpoints
export const projectApi = createApi({
  reducerPath: 'projectApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${HOST}/api`,
    mode: 'cors',
    headers: { Authorization: `Bearer ${TOKEN}`}
}),
  endpoints: (builder) => ({
    getProjectById: builder.query<Project, string>({
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
    deleteProject: builder.mutation<void, string>({
      query: (projectId) => ({
        url: `projects/${projectId}`,
        method: 'DELETE',
      }),
    }),
    createWorkspace: builder.mutation<Workspace, Partial<Workspace>>({
      query: (newWorkspace) => ({
        url: `workspaces/`,
        method: 'POST',
        body: newWorkspace,
      }),
    }),
    deleteWorkspace: builder.mutation<void, string>({
      query: (workspaceId) => ({
        url: `workspaces/${workspaceId}`,
        method: 'DELETE',
      }),
    })
})});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useGetProjectByIdQuery,
  useGetUserWorkspacesQuery,
  useGetProjectsForWorkspaceQuery,
  useCreateProjectMutation,
  useDeleteProjectMutation,
  useCreateWorkspaceMutation,
  useDeleteWorkspaceMutation
 } = projectApi;