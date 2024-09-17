import { Avatar,Text, Tooltip } from "@mantine/core";
import { User } from "../../types/User";
import { ProjectWithDetails } from "../../types/Project";

export function ProjectAssignedUsers({ project } : { project : ProjectWithDetails }) {
    const getUsersFromAllTasks = (maxNumOfUsers = 5) : User[] => {
        if (!project?.projectTasks) return [];

        const users : User[] = [];
        const userIds = new Set<string>(); 
    
        for (const task of project.projectTasks) {
            if (task.taskWorkers === null || task.taskWorkers == undefined) continue;
            for (const user of task.taskWorkers) {
                if (userIds.size >= maxNumOfUsers) break;
                if (userIds.has(user.id)) continue;
                userIds.add(user.id);
                users.push(user);
            }
        }

        return users;
    }

    const numOfUsersAssignedToTasks = () : number => {
        if (!project?.projectTasks) return 0;

        const users = new Set<string>();
        for (const task of project.projectTasks) {
            if (task.taskWorkers === null || task.taskWorkers == undefined) continue;
            for (const user of task.taskWorkers) {
                users.add(user.id);
            }
        }

        return users.size;
    }

    const MAX_NUM_AVATARS_TO_DISPLAY = 5;
    const users = getUsersFromAllTasks(MAX_NUM_AVATARS_TO_DISPLAY);
    const restNumOfUsers = numOfUsersAssignedToTasks() - users.length;

    if (users.length === 0) return <><Text size="sm">No users assigned</Text></>;

    return (
        <Avatar.Group>
            {users.map((user : User) => (
                <Tooltip key={user.id + '_tooltip'} label={`${user.firstName} ${user.lastName}`}>
                <Avatar alt={`${user.firstName} ${user.lastName}`} key={user.id} src={user.avatarUrl} radius="xl" />
                </Tooltip>
            ))}
            { restNumOfUsers > 0 && <Avatar radius="xl">{restNumOfUsers}</Avatar>}
        </Avatar.Group>
    );
}
