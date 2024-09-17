import { User } from "./User";

export interface Project {
    workspaceId: string;
    id: string;
    name: string;
    description: string;
    projectTasks: ProjectTask[];
}

export interface ProjectWithDetails
{
    workspaceId: string;
    id: string;
    name: string;
    description: string;
    projectTasks: ProjectTaskWithDetails[];
}

export interface ProjectTask {
    id: string,
    name: string,
    description: string
}

export interface ProjectTaskWithDetails {
    id: string,
    name: string,
    description: string;
    taskWorkers: User[];
}

export interface Workspace {
    id: string,
    name: string,
    description: string,
    projectIds: string[]
}

export interface WorkspaceProjects {
    id: string,
    projects: Project[]
}