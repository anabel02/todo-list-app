import React from 'react';
import { Todo } from '../types/type';
import { Button, Checkbox, Input, Table, Text, Tooltip } from '@mantine/core';
import { IconPencil, IconTrash } from '@tabler/icons-react';

export const ToDoListItem = ({ todo } : { todo: Todo }) => {
    console.log(todo);
    const {Task} = todo;
    console.log(Task);
  return (
    <Table.Tr key={todo.Id}>

    <Table.Td>
      <Checkbox onChange={(event) => console.log} />
    </Table.Td>

    <Table.Td>
      <Tooltip label={"completed \n created"}>
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
