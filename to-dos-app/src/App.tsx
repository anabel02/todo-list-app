import './App.css';
import { ToDoList } from './components/ToDoList';
import { useFetchToDos } from './hooks/useFetch';

const baseUrl = "http://localhost:5028/ToDos"
// const httpClient = new FetchClient();
// const service = new ToDoService(httpClient, baseUrl, "");

// const builder = async () => await service.query((builder, qtodo) => builder);

function App() {
  const completedFilter = `?$filter=not(CompletedDateTime eq null)`;
  const notCompletedFilter = `?$filter=(CompletedDateTime eq null)`;

  const completedTodos = useFetchToDos(completedFilter);
  const notCompletedTodos = useFetchToDos(notCompletedFilter);
  
  return (
  <>
    <ToDoList todos={ completedTodos }/>
    <ToDoList todos={ notCompletedTodos }/>
  </>
  );
}

export default App;
