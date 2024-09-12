/* eslint-disable react/prop-types */
/* eslint-disable @typescript-eslint/no-unsafe-call */
/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import React, {useEffect} from 'react';
import classNames from 'classnames';
import type {DraggableSyntheticListeners} from '@dnd-kit/core';
import type {Transform} from '@dnd-kit/utilities';

import styles from './Item.module.css';
import { Task } from '../../Task/Task';
import { ProjectTask } from '../../../types/Project';

export interface ItemProps {
  onDelete: () => void;
  dragOverlay?: boolean;
  color?: string;
  disabled?: boolean;
  dragging?: boolean;
  handle?: boolean;
  handleProps?: any;
  height?: number;
  projectId: string;
  taskId: string;
  index?: number;
  fadeIn?: boolean;
  content: ProjectTask;
  transform?: Transform | null;
  listeners?: DraggableSyntheticListeners;
  sorting?: boolean;
  style?: React.CSSProperties;
  transition?: string | null;
  wrapperStyle?: React.CSSProperties;
  value: React.ReactNode;
  onRemove?(): void;
  renderItem?(args: {
    onUpdate: () => void;
    onDelete: () => void;
    dragOverlay: boolean;
    dragging: boolean;
    sorting: boolean;
    index: number | undefined;
    fadeIn: boolean;
    listeners: DraggableSyntheticListeners;
    ref: React.Ref<HTMLElement>;
    style: React.CSSProperties | undefined;
    transform: ItemProps['transform'];
    transition: ItemProps['transition'];
    value: ItemProps['value'];
  }): React.ReactElement;
}

export const Item = React.memo(
  React.forwardRef<HTMLLIElement, ItemProps>(
    (
      {
        onUpdate,
        onDelete,
        dragOverlay,
        dragging,
        disabled,
        fadeIn,
        projectId,
        taskId,
        handle,
        index,
        listeners,
        sorting,
        style,
        content,
        transition,
        transform,
        wrapperStyle,
        ...props
      },
      ref
    ) => {
      
      useEffect(() => {

        if (!dragOverlay) {
          return;
        }

        document.body.style.cursor = 'grabbing';

        return () => {
          document.body.style.cursor = '';
        };
      }, [dragOverlay]);

      return (
        <li
          className={classNames(
            styles.Wrapper,
            fadeIn && styles.fadeIn,
            sorting && styles.sorting,
            dragOverlay && styles.dragOverlay
          )}
          style={
            {
              ...wrapperStyle,
              transition: [transition, wrapperStyle?.transition]
                .filter(Boolean)
                .join(', '),
              '--translate-x': transform
                ? `${Math.round(transform.x)}px`
                : undefined,
              '--translate-y': transform
                ? `${Math.round(transform.y)}px`
                : undefined,
              '--scale-x': transform?.scaleX
                ? `${transform.scaleX}`
                : undefined,
              '--scale-y': transform?.scaleY
                ? `${transform.scaleY}`
                : undefined,
              '--index': index,
            } as React.CSSProperties
          }
          {...props}
          ref={ref}
        >
          <div
            className={classNames(
              styles.Item,
              dragging && styles.dragging,
              dragOverlay && styles.dragOverlay,
              disabled && styles.disabled,
            )}
            style={style}
            data-cypress="draggable-item"
            {...(!handle ? listeners : undefined)}
            tabIndex={!handle ? 0 : undefined}
          >
            <Task onUpdate={onUpdate} onDelete={onDelete} key={content.id} projectId={projectId} id={taskId} name={content.name} description={content.description} />
              <span className={styles.Actions}>
            </span>
          </div>
        </li>
      );
    }
  )
);
