import { Table, Progress, Anchor, Text, Group, ActionIcon, Avatar } from '@mantine/core';
import classes from './WorkspaceProjectsTable.module.css';
import { IconTrash } from '@tabler/icons-react';
import { Project } from '../../types/Project';

const data = [
  {
    title: 'Foundation',
    author: 'Isaac Asimov',
    year: 1951,
    reviews: { positive: 2223, negative: 259 },
  },
  {
    title: 'Frankenstein',
    author: 'Mary Shelley',
    year: 1818,
    reviews: { positive: 5677, negative: 1265 },
  },
  {
    title: 'Solaris',
    author: 'Stanislaw Lem',
    year: 1961,
    reviews: { positive: 3487, negative: 1845 },
  },
  {
    title: 'Dune',
    author: 'Frank Herbert',
    year: 1965,
    reviews: { positive: 8576, negative: 663 },
  },
  {
    title: 'The Left Hand of Darkness',
    author: 'Ursula K. Le Guin',
    year: 1969,
    reviews: { positive: 6631, negative: 993 },
  },
  {
    title: 'A Scanner Darkly',
    author: 'Philip K Dick',
    year: 1977,
    reviews: { positive: 8124, negative: 1847 },
  },
];

interface WorkspaceProjectsTableProps {
    projects: Project[];
}

export function WorkspaceProjectsTable({projects} : WorkspaceProjectsTableProps ) {
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
          <Anchor component="button" fz="sm">
          <Avatar
          src="https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-8.png"
          radius="xl"
        />
          </Anchor>
        </Table.Td>
        <Table.Td>
          <Progress.Root>
            <Progress.Section
              className={classes.progressSection}
              value={10}
              color="teal"
            />

            <Progress.Section
              className={classes.progressSection}
              value={90}
              color="red"
            />
          </Progress.Root>
        </Table.Td>
        <Table.Td>
            <ActionIcon variant="transparent">
                <IconTrash />
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
            <Table.Th>Reviews distribution</Table.Th>
            <Table.Th>Delete</Table.Th>
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows}</Table.Tbody>
      </Table>
    </Table.ScrollContainer>
  );
}