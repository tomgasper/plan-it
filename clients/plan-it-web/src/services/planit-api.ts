import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

import type { Project } from '../types/Project'

const HOST = "https://localhost:5234";

// Define a service using a base URL and expected endpoints
export const projectApi = createApi({
  reducerPath: 'projectApi',
  baseQuery: fetchBaseQuery({
    baseUrl: `${HOST}/api`,
    mode: 'cors',
}),
  endpoints: (builder) => ({
    getProjectById: builder.query<Project, string>({
      query: (id) => `projects/${id}`,
    }),
  }),
})

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetProjectByIdQuery } = projectApi;