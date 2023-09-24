import { useState } from 'react';
import { Todo } from '../types/type';
import { Button, Checkbox, Table, Text, TextInput, Tooltip } from '@mantine/core';
import { IconPencil, IconTrash } from '@tabler/icons-react';
import { useAppDispatch } from '../store/store';
import { applyFilter, completeTodo, editTodo,  removeTodo } from '../store/actionsHandlers';
import moment from 'moment';
import { confirm, validate } from '../helpers/confirmation';

export const ToDoListItem = ({ todo }: { todo: Todo }) => {
  const dispatch = useAppDispatch();

  const [checked, setChecked] = useState(todo.CompletedDateTime !== null);

  const handleCompleteTodo = async () => {
    await dispatch(completeTodo(todo));
    setChecked(true);
  }

  const handleConfirmComplete = (e: any) => {
    e.preventDefault();
    confirm(handleCompleteTodo, () => { }, "Complete", "blue");
  }

  const handleDeleteTodo = async () => {
    await dispatch(removeTodo(todo));
  }

  const handleConfirmDelete = (e: any) => {
    e.preventDefault();
    confirm(handleDeleteTodo, () => { }, "Delete", "red");
  }

  const [editedTodo, setEditedTodo] = useState("");
  const [name, setName] = useState(todo.Task);

  const handleEditTodo = async () => {
    await dispatch(editTodo({ ...todo, Task: editedTodo }));
    setEditedTodo("");
    setName(editedTodo);
  }

  const handleEditInputChange = (event: any) => {
    event.preventDefault();
    setEditedTodo(event.target.value);
  };

  const handleCancelEdit = () => {
    setEditedTodo("");
  };

  const handleConfirmEdit = (e: any) => {
    e.preventDefault();
    if (validate(editedTodo)) {
      confirm(handleEditTodo, handleCancelEdit, "Edit", "blue");
    }
  }

  return (
    <Table.Tr key={todo.Id}>

      <Table.Td>
        {
          (checked && <Checkbox disabled checked={checked} />)
          || <Checkbox onChange={handleConfirmComplete} />
        }
      </Table.Td>

      <Table.Td>
        <Tooltip label={`Created at: ${moment(todo.CreatedDateTime).format("DD/MM/YYYY HH:mm:ss")} ${checked ? 'Completed at: ' + moment(todo.CompletedDateTime).format("DD/MM/YYYY HH:mm:ss") : ''}`}
          multiline
          w={250} withArrow
          transitionProps={{ duration: 300 }}
          position="top-start"
        >
          <Text size='md' mt='sm'> {name} </Text>
        </Tooltip>
      </Table.Td>

      <Table.Td>
        <Button variant="filled" rightSection={<IconTrash size={14} />} color="rgba(212, 61, 61, 1)" radius="lg" onClick={handleConfirmDelete}>
          Delete
        </Button>
      </Table.Td>

      <Table.Td>
        <form onSubmit={handleConfirmEdit}>
          <Tooltip disabled={todo.CompletedDateTime === null} label={`You can't edit a completed task.`}
            withArrow
            transitionProps={{ duration: 300 }}
            position="top-start">
            <TextInput
              disabled={checked}
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
