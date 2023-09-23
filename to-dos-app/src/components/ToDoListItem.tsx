import React, { useEffect, useState } from 'react';
import { Todo } from '../types/type';
import { Button, Checkbox, Input, Table, Text, Tooltip } from '@mantine/core';
import { IconPencil, IconTrash } from '@tabler/icons-react';
import { useAppDispatch } from '../store/store';
import { completeTodo } from '../store/actionsCreator';

export const ToDoListItem = ({ todo } : { todo: Todo }) => {
  const dispatch = useAppDispatch();

  const [checked, setChecked] = useState(false);

  return (
    <Table.Tr key={todo.Id}>
    
    <Table.Td>
    { 
      (todo.CompletedDateTime !== null && <Checkbox disabled checked={ todo.CompletedDateTime !== null }
        /> ) 
      || <Checkbox onChange={(event) => {setChecked(event.currentTarget.checked)}} />
    }
    </Table.Td>

    <Table.Td>
      <Tooltip label={`Created at: ${todo.CreatedDateTime} ${todo.CompletedDateTime !== null ? 'Completed at: ' + todo.CreatedDateTime : ''}`}
        multiline 
        w={350} withArrow
        transitionProps={{ duration: 300 }}
        color="blue" 
        position="top-start"
        >
         <Text size='md' mt='sm'> { todo.Task } </Text>
      </Tooltip>
    </Table.Td>

    <Table.Td>
        <Button variant="filled" rightSection={<IconTrash size={14} />} color="rgba(212, 61, 61, 1)" radius="lg">
        Delete
        </Button>
    </Table.Td>

    <Table.Td>
        <Input rightSection={<IconPencil size={14} />}
        placeholder="Edit task"
        radius="xl"
        onSubmit={() => (console.log)}
        />
        </Table.Td>
    </Table.Tr>
  )
}
