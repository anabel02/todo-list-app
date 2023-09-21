import { List, ThemeIcon } from '@mantine/core';
import { IconCircleCheck, IconCircleDashed } from '@tabler/icons-react';

import React from 'react'
import { Todo } from '../types/todo';
import { ToDoListItem } from './ToDoListItem';

export const ToDoList = (todos: Todo[]) => {
    return (
        <List
          spacing="xs"
          size="sm"
          center
        > { todos.map(todo => <ToDoListItem key={todo.id} 
                                            id={todo.id}
                                            task={todo.task}
                                            createdDateTime={todo.createdDateTime}
                                            completedDateTime={todo.completedDateTime}/>)}
        </List>
      );
}