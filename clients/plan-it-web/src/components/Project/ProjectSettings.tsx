import React, { useState, useEffect, useCallback } from "react";
import { Button, Flex, Group, Loader, Stack, Textarea, TextInput } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { notifications } from '@mantine/notifications';
import { useLazyGetProjectWithDetailsQuery, useUpdateProjectMutation } from "../../services/planit-api";
import { Project } from "../../types/Project";
import classes from "./projectSettings.module.css";

interface ProjectSettingsProps {
  onUpdate: (project: Project) => void;
  onRemove: () => void;
  projectId: string;
}

export function ProjectSettings({
  onUpdate,
  onRemove,
  projectId,
  workspaceId
}: ProjectSettingsProps) {
  const [projectName, setProjectName] = useState("");
  const [projectDescription, setProjectDescription] = useState("");

  const [getProject, { data: project, error: projectError, isLoading: projectLoading }] = useLazyGetProjectWithDetailsQuery();
  const [updateProject, { isLoading: projectUpdating }] = useUpdateProjectMutation();

  const fetchProject = useCallback(() => {
    getProject(projectId);
  }, [getProject, projectId]);

  useEffect(() => {
    fetchProject();
  }, [fetchProject, workspaceId]);

  useEffect(() => {
    if (project) {
      setProjectName(project.name);
      setProjectDescription(project.description);
    }
  }, [project]);

  const handleSaveProject = async () => {
    const updatedProject = {
      name: projectName,
      description: projectDescription
    };

    try {
      const result = await updateProject({ updatedProject, projectId }).unwrap();
      const projectWithDetails = await getProject(projectId).unwrap();
      onUpdate(projectWithDetails);
      notifications.show({
        title: 'Success',
        message: 'Project settings saved successfully',
        color: 'green'
      });
    } catch (error) {
      console.error('Error updating project:', error);
      notifications.show({
        title: 'Error updating project',
        message: 'An error occurred while updating the project.',
        color: 'red'
      });
    }
  };

  if (projectLoading) return <Loader />;
  if (projectError) return <div>Error loading project</div>;

  return (
    <Flex className={classes.container} justify="center" align="center">
      <Stack mb={25}>
        <Stack>
          <TextInput
            label="Name"
            placeholder="Project name"
            required
            value={projectName}
            onChange={(e) => setProjectName(e.currentTarget.value)}
          />
          <Textarea
            autosize
            minRows={3}
            label="Description"
            placeholder="Project description"
            required
            value={projectDescription}
            onChange={(e) => setProjectDescription(e.currentTarget.value)}
          />
        </Stack>
        <Group justify="space-between">
          <Button variant='light' color='red' onClick={onRemove}>Delete</Button>
          <Button color="blue" onClick={handleSaveProject} loading={projectUpdating}>Save</Button>
        </Group>
      </Stack>
    </Flex>
  );
}