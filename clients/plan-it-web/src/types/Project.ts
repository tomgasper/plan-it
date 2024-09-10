export interface Project {
    workspaceId: string;
    id: string;
    name: string;
    description: string;
    projectTasks: ProjectTask[];
}

export interface ProjectTask {
    id: string,
    name: string,
    description: string
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