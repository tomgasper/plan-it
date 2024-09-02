
import '@mantine/core/styles.css';

import { MantineProvider } from '@mantine/core';
import { Navbar } from './components/Navbar/Navbar';

import './App.css';
import { MainWindow } from './components/MainWindow/MainWindow';
import { Provider } from 'react-redux';
import { store } from './redux/store';

export default function App() {
  return (
    <Provider store={store}>
    <MantineProvider>
      <Navbar />
      <MainWindow />
    </MantineProvider>
    </Provider>
  );
};