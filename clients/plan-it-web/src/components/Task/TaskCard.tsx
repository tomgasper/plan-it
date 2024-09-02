import { Card, Avatar, Text, Progress, Badge, Group, ActionIcon, useMantineTheme, Stack } from '@mantine/core';
import { IconUpload } from '@tabler/icons-react';
import classes from './TaskCard.module.css';

interface TaskCardProps
{
  id: number;
};

export function TaskCard( {id} : TaskCardProps) {
  const theme = useMantineTheme();

  return (
    <Card pt={0} pl={10} pb={15} className={classes.container} >
      <Stack gap={5}>
      <Text pt={0} fz="lg" fw={500} mt="sm">
        Task {id}
      </Text>
      <Text c={"#545454"} fz="sm" mt={5}>
        Form context management, Switch, Grid and Indicator components improvements, new hook and
        10+ other changes
      </Text>

      <Group mt={10}>
        <Badge>12 days left</Badge>
        <Badge color={theme.colors.red[6]}>High priority</Badge>
        <Badge>Some other badge</Badge>
      </Group>
      </Stack>
    </Card>
  );
}