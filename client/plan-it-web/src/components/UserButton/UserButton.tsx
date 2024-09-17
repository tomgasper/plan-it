import { UnstyledButton, Group, Avatar, Text } from '@mantine/core';
import { IconChevronRight } from '@tabler/icons-react';
import classes from './UserButton.module.css';
import { useAppSelector } from '../../hooks/reduxHooks';
import { useGetUserQuery } from '../../services/planit-api';
import { UserFromJwt } from '../../types/User';

interface UserButton {
  onClick: () => void;
}

export function UserButton({onClick} : UserButton) {
  const user : UserFromJwt | null = useAppSelector(state => state.auth.user);
  const { data:userDetails } = useGetUserQuery(user?.id ?? '');

  return (
    <UnstyledButton onClick={onClick} className={classes.user}>
      <Group className={classes.container}>
        <Avatar
          src={userDetails?.avatarUrl ?? ''}
          radius="xl"
        />

        <div style={{ flex: 1 }}>
          <Text size="sm" fw={500}>
            {user?.firstName} {user?.lastName}
          </Text>
        </div>

        <IconChevronRight style={{ width: String(14), height: String(14) }} stroke={1.5} />
      </Group>
    </UnstyledButton>
  );
}