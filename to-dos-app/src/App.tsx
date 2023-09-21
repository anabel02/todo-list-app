import React, { useReducer } from 'react';
import logo from './logo.svg';
import './App.css';
import { Box, Button, TextInput } from '@mantine/core';
import { useFetch } from './helpers/fetch';

function App() {
  const x = useFetch(`http://localhost:5028/ToDo`, []);
  console.log();
  return (
    <div className="App">
    </div>
  );
}

export default App;
