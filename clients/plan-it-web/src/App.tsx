
import '@mantine/core/styles.css';

import { Navbar } from './components/Navbar/Navbar';

import './App.css';
import { MainWindow } from './components/MainWindow/MainWindow';
import { useGetUserWorkspacesQuery } from './services/planit-api';

import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { useAppDispatch } from './hooks/reduxHooks';
import { useEffect } from 'react';
import { setWorkspaces } from './redux/workspacesSlice';
import { WorkspaceSettings } from './components/WorkspaceSettings/WorkspaceSettings';

export default function App() {
  const dispatch = useAppDispatch();
  const { data, isLoading, error } = useGetUserWorkspacesQuery('e0d91303-b5c9-4530-9914-d27c7a054415');

  useEffect(() => {
    if (data) {
      dispatch(setWorkspaces(data));
    }
  },[data, dispatch]);

  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error occurred while fetching workspaces</div>;

  return (
    <>
    <BrowserRouter>
      <Navbar />
        <Routes>
          <Route path="/workspaces/:workspaceId/settings" element={<WorkspaceSettings />} />
          <Route path="/workspaces/:workspaceId" element={<MainWindow />} />
        </Routes>
      </BrowserRouter>
    </>
  );
};