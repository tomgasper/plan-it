import React, { useState } from "react";
import { Button, Flex, Group, Loader, Stack, TextInput } from "@mantine/core";
import { notifications } from '@mantine/notifications';
import { useGetProjectQuery, useCreateProjectTaskMutation } from "../../services/planit-api";
import classes from "./NewTaskModal.module.css";
import { ProjectTask } from "../../types/Project";

interface NewTaskModalProps {
  onClose: (task : ProjectTask) => void;
  closeWindow: () => void;
  projectId: string;
}

export function NewTaskModal({ onClose, closeWindow, projectId }: NewTaskModalProps) {
  const [taskName, setTaskName] = useState("");
  const [taskDescription, setTaskDescription] = useState("");
  const [createProjectTask, { isLoading: taskCreating }] = useCreateProjectTaskMutation();

  const handleAddTask = async () => {
    if (!taskName || !taskDescription) {
      notifications.show({
        title: 'Validation Error',
        message: 'Please fill in both name and description',
        color: 'red'
      });
      return;
    }

    try {
      const result = await createProjectTask({
        projectId,
        task: {
          name: taskName,
          description: taskDescription
        }
      }).unwrap();

      notifications.show({
        title: 'Success',
        message: 'Task added successfully',
        color: 'green'
      });
      console.log('Task created:', result);
      onClose(result);
      closeWindow();
    } catch (error) {
      console.error('Error creating task:', error);
      notifications.show({
        title: 'Error creating task',
        message: error.data?.title || 'An unexpected error occurred',
        color: 'red'
      });
    }
  };

  return (
    <Flex className={classes.container} justify="center" align="center">
      <Stack mb={25}>
        <Stack>
          <TextInput
            label="Name"
            placeholder="Enter task name"
            required
            value={taskName}
            onChange={(e) => setTaskName(e.currentTarget.value)}
          />
          <TextInput
            label="Description"
            placeholder="Enter task description"
            required
            value={taskDescription}
            onChange={(e) => setTaskDescription(e.currentTarget.value)}
          />
        </Stack>
        <Group justify="space-between">
          <Button variant="light" color="red" onClick={closeWindow}>
            Cancel
          </Button>
          <Button color="blue" onClick={handleAddTask} loading={taskCreating}>
            Add Task
          </Button>
        </Group>
      </Stack>
    </Flex>
  );
}