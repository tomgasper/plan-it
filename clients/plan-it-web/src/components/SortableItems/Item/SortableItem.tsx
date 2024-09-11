import { useSortable } from '@dnd-kit/sortable';
import { Item } from './Item';
import { ProjectTask } from '../../../types/Project';

interface SortableItemProps {
  onDeleteTask: () => void;
  onUpdateTask: () => void;
  containerId: string;
  id: string;
  index: number;
  handle: boolean;
  disabled?: boolean;
  getIndex(id: string): number;
  content: ProjectTask;
}

export function SortableItem({
  onUpdateTask,
  onDeleteTask,
  disabled,
  projectId,
  id,
  index,
  handle,
  content,
}: SortableItemProps) {
  const {
    setNodeRef,
    setActivatorNodeRef,
    listeners,
    isDragging,
    isSorting,
    transform,
    transition,
  } = useSortable({
    id,
  });

  return (
    <Item
      onUpdate={onUpdateTask}
      onDelete={onDeleteTask}
      ref={disabled ? undefined : setNodeRef}
      value={id}
      taskId={id}
      dragging={isDragging}
      sorting={isSorting}
      handle={handle}
      handleProps={handle ? {ref: setActivatorNodeRef} : undefined}
      index={index}
      projectId={projectId}
      content={content}
      transition={transition}
      transform={transform}
      listeners={listeners}
    />
  );
}