import { TextInput, Button, Group, Box } from '@mantine/core';
import { useState } from 'react';
import { useAppDispatch } from '../store/store';
import { addTodo } from '../store/actionsCreator';
import { confirm, validate } from '../helpers/confirmation';

export const AddToDo = () => {
  const dispatch = useAppDispatch();

  const [newTodo, setNewTodo] = useState("");

  const handleAddTodo = () => {
    dispatch(addTodo(newTodo));
    setNewTodo("");
    window.location.reload();
  };

  const handleCancelAdd = () => {
    setNewTodo("");
  }

  const handleSubmit = (e: any) => {
    e.preventDefault();
    if (validate(newTodo)) {
      confirm(handleAddTodo, handleCancelAdd, "Add")
    }
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    e.preventDefault();
    setNewTodo(e.target.value);
  };

  return (
    <Box maw={340} h={100} mx="auto">
      <form onSubmit={handleSubmit}>
        <TextInput
          onChange={handleInputChange}
          value={newTodo}
        />

        <Group justify="flex-end" mt="md">
          <Button type='submit'>Add To Do</Button>
        </Group>
      </form>
    </Box>
  );
}