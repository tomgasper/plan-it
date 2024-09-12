import { ActionIcon, Group, Menu } from "@mantine/core";
import { IconSettings, IconTrash } from "@tabler/icons-react";
import { useAppDispatch } from "../../hooks/reduxHooks";
import { Navigate, useNavigate, useParams } from "react-router-dom";
import { useDeleteWorkspaceMutation } from "../../services/planit-api";
import { deleteWorkspaceLocal } from "../../redux/workspacesSlice";

export function WorkspaceMenu()
{
    const { workspaceId } = useParams<{ workspaceId: string }>();
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const [ deleteWorkspace ] = useDeleteWorkspaceMutation();

    const handleDeleteWorkspace = async () => {
        console.log("calling!");
        if (!workspaceId) return;

        if (!window.confirm('Are you sure you want to delete this workspace?')) return;

        const result = await deleteWorkspace(workspaceId);

        if (result.error)
        {
            console.error('Error deleting workspace:', result.error);
        }

        dispatch(deleteWorkspaceLocal(workspaceId));
        navigate('/');
    }

    const handleWorkspaceSettings = () => {
        navigate(`/workspaces/${workspaceId}/settings`);
    }

    return (
        <Menu>
            <Menu.Target>
                <ActionIcon size={42} variant="transparent" >
                        <IconSettings style={{ width: '70%', height: '70%' }} stroke={1.4} />
                    </ActionIcon>
            </Menu.Target>

            <Menu.Dropdown>
                <Menu.Item onClick={handleWorkspaceSettings}>
                    Workspace Settings
                </Menu.Item>
                <Menu.Item>
                    Option 2
                </Menu.Item>
                <Menu.Item leftSection={
                    <IconTrash />}
                    onClick={handleDeleteWorkspace}
                >
                        Delete Workspace
                </Menu.Item>
            </Menu.Dropdown>

        </Menu>
    )
}