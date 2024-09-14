import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import type { Project, ProjectTask, Workspace, WorkspaceProjects } from '../types/Project'
import { User } from '../types/User';

const HOST = "https://localhost:5234";

// Define a service using a base URL and expected endpoints
export const projectApi = createApi({
  reducerPath: 'projectApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${HOST}/api`,
    mode: 'cors',
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).auth.token;
      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
      }
      return headers;
    },
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
    })}),
    // User
    getUser: builder.query<User, string>({
      query: (userId) => `users/${userId}`,
    }),
    getUsers: builder.query<User[], void>({
      query: () => `users`,
    }),
    updateUser: builder.mutation<User, Partial<User> & { userId: string }>({
      query: ({ userId, ...patch }) => ({
        url: `/users/${userId}`,
        method: 'PATCH',
        body: patch,
      }),
    }),
    uploadAvatar: builder.mutation<{ avatarUrl: string }, { userId: string, avatar: FormData }>({
      query: ({ userId, avatar }) => ({
        url: `/users/${userId}/avatar`,
        method: 'PATCH',
        body: avatar,
      }),
    }),
  }),
});

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
  useGetProjectTasksQuery,
  useGetUserQuery,
  useGetUsersQuery,
  useUpdateUserMutation,
  useUploadAvatarMutation
 } = projectApi;