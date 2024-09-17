import { Table, Progress, Anchor, Text, ActionIcon } from '@mantine/core';
import classes from './WorkspaceProjectsTable.module.css';
import { IconTrash } from '@tabler/icons-react';
import { ProjectWithDetails } from '../../types/Project';
import { ProjectAssignedUsers } from '../Project/ProjectAssignedUsers';

interface WorkspaceProjectsTableProps {
    onDeleteProject: (projectId : string) => Promise<void>;
    projects: ProjectWithDetails[];
}

export function WorkspaceProjectsTable({projects, onDeleteProject} : WorkspaceProjectsTableProps ) {
  const rows = projects.map((row) => {
    return (
      <Table.Tr key={row.id}>
        <Table.Td>
          <Anchor component="button" fz="sm">
            {row.name}
          </Anchor>
        </Table.Td>
        <Table.Td>{row.description}</Table.Td>
        <Table.Td>
          { <ProjectAssignedUsers project={row} /> ?? <Text>-</Text> }
        </Table.Td>
        <Table.Td>
          <Progress.Root>
            <Progress.Section
              className={classes.progressSection}
              value={0}
              color="teal"
            />
          </Progress.Root>
        </Table.Td>
        <Table.Td>
            <ActionIcon variant="transparent">
                <IconTrash onClick={ (_e) => onDeleteProject(row.id) } />
            </ActionIcon>
        </Table.Td>
      </Table.Tr>
    );
  });

  return (
    <Table.ScrollContainer minWidth={800}>
      <Table verticalSpacing="xs">
        <Table.Thead>
          <Table.Tr>
            <Table.Th>Title</Table.Th>
            <Table.Th>Description</Table.Th>
            <Table.Th>Assigned users</Table.Th>
            <Table.Th>Completed tasks</Table.Th>
            <Table.Th>Delete</Table.Th>
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows}</Table.Tbody>
      </Table>
    </Table.ScrollContainer>
  );
}