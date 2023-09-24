import { TextInput, Button, Group, Box } from '@mantine/core';
import { useState } from 'react';
import { useAppDispatch } from '../store/store';
import { addTodo } from '../store/actionsHandlers';
import { confirm, validate } from '../helpers/confirmation';
import { IconPlus } from '@tabler/icons-react';

export const AddToDo = () => {
  const dispatch = useAppDispatch();

  const [newTodo, setNewTodo] = useState("");

  const handleAddTodo = async () => {
    await dispatch(addTodo(newTodo));
    setNewTodo("");
  };

  const handleCancelAdd = () => {
    setNewTodo("");
  }

  const handleSubmit = (e: any) => {
    e.preventDefault();
    if (validate(newTodo)) {
      confirm(handleAddTodo, handleCancelAdd, "Add", "blue")
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
          size="md"
          placeholder='New task'
        />

        <Group  mt="md">
          <Button type='submit'
          rightSection={<IconPlus size={14} />}>Add</Button>
        </Group>
      </form>
    </Box>
  );
}