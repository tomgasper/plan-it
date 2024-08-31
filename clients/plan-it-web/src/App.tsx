
import '@mantine/core/styles.css';

import { MantineProvider } from '@mantine/core';
import { Navbar } from './components/Navbar/Navbar';

import './App.css';
import { MainWindow } from './components/MainWindow/MainWindow';

export default function App() {
  return (
    <MantineProvider>
      <Navbar />
      <MainWindow />
    </MantineProvider>
  );
};