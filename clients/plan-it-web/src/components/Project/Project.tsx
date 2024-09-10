import classes from './Project.module.css';
import { Avatar, Group, Progress, Stack, Title, Text, Loader } from "@mantine/core";
import { Handle, Remove } from "../SortableItems/Item";
import { useDeleteProjectMutation } from '../../services/planit-api';

const avatars = [
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-2.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-4.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-7.png',
  ];

interface ProjectProps {
    handleProps : React.HTMLAttributes<unknown> | undefined;
    onRemove: () => void;
    name: string;
    description: string;
}

export function Project({ onRemove, handleProps, name, description, id } : ProjectProps ) {

  return (
        <Stack className={classes.headerContainer} align="stretch">
        <Group justify="space-between">
        <Title order={3} >{name + id} </Title>
            <Group>
                <Remove onClick={onRemove} />
                <Handle {...handleProps} />
            </Group>
        </Group>
        <Progress autoContrast value={(23 / 36) * 100} mt={5} />
        <Group justify="space-between">
        <Text fz="sm" mt="md">
        Tasks completed:{' '}
            <Text span fw={500} c="bright">
                23/36
            </Text>
        </Text>
        <Avatar.Group>
            <Avatar src={avatars[0]} radius="xl" />
            <Avatar src={avatars[1]} radius="xl" />
            <Avatar src={avatars[2]} radius="xl" />
            <Avatar radius="xl">+5</Avatar>
        </Avatar.Group>
        </Group>
    </Stack>
  );
}