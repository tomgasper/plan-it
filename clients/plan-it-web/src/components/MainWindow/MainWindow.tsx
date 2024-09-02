import { Flex, Title } from "@mantine/core";
import { MultipleContainers } from '../SortableItems/ManyItems';
import classes from './MainWindow.module.css';
import { useGetProjectByIdQuery } from "../../services/planit-api";

export function MainWindow() {
    const { data, error, isLoading } = useGetProjectByIdQuery('d0b41044-c9c0-40d3-9750-b1a72d4acbf4');

    return (
        <Flex className={classes.container} direction="column">
            { isLoading && <div>Loading...</div>} {data?.name}
            <Title>Workspace</Title>
            <MultipleContainers />
        </Flex>
    );  
}