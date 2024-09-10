import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { Provider } from 'react-redux'
import { MantineProvider } from '@mantine/core'
import { store } from './redux/store.ts'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
    <MantineProvider>
      <App />
    </MantineProvider>
    </Provider>
  </StrictMode>,
)
