import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { Provider } from 'react-redux'
import { MantineProvider } from '@mantine/core'
import { Notifications } from '@mantine/notifications';
import '@mantine/notifications/styles.css';
import { store } from './redux/store.ts'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
    <MantineProvider>
      <Notifications />
          <App />
    </MantineProvider>
    </Provider>
  </StrictMode>,
)
