import React, { useReducer } from 'react';
import logo from './logo.svg';
import './App.css';
import { Box, Button, TextInput } from '@mantine/core';
import { useFetch } from './helpers/fetch';
import { createQueryBuilderV4  } from "@odata2ts/odata-query-builder";
import { ToDoService } from "../build/trippin/service/ToDoService";
import { FetchClient } from "@odata2ts/http-client-fetch";

function App() {

  // the generate main service


const baseUrl = "http://localhost:5028/ToDos"
const httpClient = new FetchClient();
const service = new ToDoService(httpClient, baseUrl, "ToDo");
  
  return (
    <div className="App">
    </div>
  );
}

export default App;
