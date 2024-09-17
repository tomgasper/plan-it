import { Button } from "@mantine/core";

export function AddNewProjectButton( {onClick} : {onClick: () => void} )
{
    return (
        <Button justify='center'
            onClick= { onClick }
          >
            + Add new project
          </Button>
    )
}