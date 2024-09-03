import { useSortable } from '@dnd-kit/sortable';
import { Item } from './Item';
import { ProjectTask } from '../../../types/Project';

interface SortableItemProps {
  containerId: string;
  id: string;
  index: number;
  handle: boolean;
  disabled?: boolean;
  getIndex(id: string): number;
  content: ProjectTask;
}

export function SortableItem({
  disabled,
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
      ref={disabled ? undefined : setNodeRef}
      value={id}
      dragging={isDragging}
      sorting={isSorting}
      handle={handle}
      handleProps={handle ? {ref: setActivatorNodeRef} : undefined}
      index={index}
      content={content}
      transition={transition}
      transform={transform}
      listeners={listeners}
    />
  );
}