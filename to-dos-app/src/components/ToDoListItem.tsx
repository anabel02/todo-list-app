import React, { useState } from 'react';
import { Todo } from '../types/type';
import { Button, Checkbox, Input, Table, Text, TextInput, Tooltip } from '@mantine/core';
import { IconPencil, IconTrash } from '@tabler/icons-react';
import { useAppDispatch } from '../store/store';
import { completeTodo, editTodo, removeTodo } from '../store/actionsCreator';

export const ToDoListItem = ({ todo }: { todo: Todo }) => {
  const dispatch = useAppDispatch();

  const handleCompleteTodo = (event: any) => {
    dispatch(completeTodo(todo));
    window.location.reload();
  }

  const handleDeleteTodo = () => {
    dispatch(removeTodo(todo));
    window.location.reload();
  }

  const [editedTodo, setEditedTodo] = useState("");

  const handleEditTodo = (event: any) => {
    dispatch(editTodo({ ...todo, Task: editedTodo }));
    window.location.reload();
  }

  const handleEditInputChange = (event: any) => {
    event.preventDefault();
    setEditedTodo(event.target.value);
  };

  return (
    <Table.Tr key={todo.Id}>

      <Table.Td>
        {
          (todo.CompletedDateTime !== null && <Checkbox disabled checked={todo.CompletedDateTime !== null}/>)
          || <Checkbox onChange={handleCompleteTodo}/>
        }
      </Table.Td>

      <Table.Td>
        <Tooltip label={`Created at: ${todo.CreatedDateTime} ${todo.CompletedDateTime !== null ? 'Completed at: ' + todo.CreatedDateTime : ''}`}
          multiline
          w={350} withArrow
          transitionProps={{ duration: 300 }}
          position="top-start"
        >
          <Text size='md' mt='sm'> {todo.Task} </Text>
        </Tooltip>
      </Table.Td>

      <Table.Td>
        <Button variant="filled" rightSection={<IconTrash size={14} />} color="rgba(212, 61, 61, 1)" radius="lg" onClick={handleDeleteTodo}>
          Delete
        </Button>
      </Table.Td>

      <Table.Td>
        <form onSubmit={handleEditTodo}>
          <Tooltip disabled={todo.CompletedDateTime === null} label={`You can't edit a completed task.`}
            withArrow
            transitionProps={{ duration: 300 }}
            position="top-start">
            <TextInput
              disabled={todo.CompletedDateTime !== null}
              rightSection={<IconPencil size={14} />}
              placeholder="Edit task"
              radius="xl"
              value={editedTodo}
              onChange={handleEditInputChange} />
          </Tooltip>
        </form>
      </Table.Td>
    </Table.Tr>
  )
}
