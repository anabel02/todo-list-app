import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import { MantineProvider, } from '@mantine/core';
import '@mantine/core/styles.css';
import { ToDoApp } from './components/ToDoApp';
import store from './store/store';
import { Provider } from 'react-redux';
import { Notifications } from '@mantine/notifications';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render( 
  <Provider store = {store}>
    <MantineProvider>
      <ModalsProvider>
        <Notifications />
        <ToDoApp />
      </ModalsProvider>
    </MantineProvider>
  </Provider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();