import { Modal, Title } from "@mantine/core";

interface ExtendedModalProps
{
    title: string;
    opened: boolean;
    onClose: () => void;
    children: React.ReactNode;
}

export function ExtendedModal({
    title,
    children,
    opened,
    onClose} : ExtendedModalProps)
{
    return (
        <>
        <Modal.Root onMouseDown={e => e.stopPropagation() } opened={opened} onClose={onClose} closeOnClickOutside={true}>
        <Modal.Overlay />
        <Modal.Content p={0} m={0}>
          <Modal.Header >
            <Title order={5}>{title}</Title>
            <Modal.CloseButton />
          </Modal.Header>
          <Modal.Body >{children}</Modal.Body>
        </Modal.Content>
      </Modal.Root>
      </>
    )
}