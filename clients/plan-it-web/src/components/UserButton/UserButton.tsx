import { UnstyledButton, Group, Avatar, Text } from '@mantine/core';
import { IconChevronRight } from '@tabler/icons-react';
import classes from './UserButton.module.css';

interface UserButton {
  onClick: () => void;
}

export function UserButton({onClick} : UserButton) {
  return (
    <UnstyledButton onClick={onClick} className={classes.user}>
      <Group className={classes.container}>
        <Avatar
          src="https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-8.png"
          radius="xl"
        />

        <div style={{ flex: 1 }}>
          <Text size="sm" fw={500}>
            Harriette Spoonlicker
          </Text>

          <Text c="dimmed" size="xs">
            hspoonlicker@outlook.com
          </Text>
        </div>

        <IconChevronRight style={{ width: String(14), height: String(14) }} stroke={1.5} />
      </Group>
    </UnstyledButton>
  );
}