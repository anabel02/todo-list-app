import { Button, Checkbox, Input, Table, Text, Tooltip } from '@mantine/core';

import React from 'react';
import { OdataResponse, Todo } from '../types/type';
import { IconPencil, IconTrash } from '@tabler/icons-react';
 import { ToDoListItem } from './ToDoListItem';

export const ToDoList = ({ todos }: { todos: Todo[]; }) => {
  return (
        <Table>
          <Table.Tbody>
            { todos.map((todo) => ( <ToDoListItem todo={todo}/>))}
          </Table.Tbody>
        </Table>
  );
}
