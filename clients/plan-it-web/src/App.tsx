
import '@mantine/core/styles.css';

import { Navbar } from './components/Navbar/Navbar';

import './App.css';
import { MainWindow } from './components/MainWindow/MainWindow';
import { useGetUserWorkspacesQuery } from './services/planit-api';

import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { useAppDispatch, useAppSelector} from './hooks/reduxHooks';
import { useEffect, useState } from 'react';
import { setWorkspaces } from './redux/workspacesSlice';
import { WorkspaceSettings } from './components/WorkspaceSettings/WorkspaceSettings';
import { Login } from './components/Login/Login';
import { useJwtAuth } from './hooks/useJwtAuth';
import { Register } from './components/Register/Register';
import { ProfilePage } from './components/Profile/ProfilePage';
import { ProtectedRoute } from './router/ProtectedRoute';
import { Flex, Loader } from '@mantine/core';
import { UserFromJwt } from './types/User';

export default function App() {
  const dispatch = useAppDispatch();
  const [isAuthChecking, setIsAuthChecking] = useState(true);
  const { isAuthenticated } = useJwtAuth();
  const user : UserFromJwt | null = useAppSelector(state => state.auth.user);
  const { data, isLoading, error } = useGetUserWorkspacesQuery(user?.id ?? '', { skip: user == null});

  console.log(isAuthenticated);

  useEffect(() => {
    if (data) {
      dispatch(setWorkspaces(data));
    }
  },[data, dispatch]);

  useEffect(() => {
    if (isAuthenticated !== null) {
      setIsAuthChecking(false);
    }
  }, [isAuthenticated]);

  if (isAuthChecking || isLoading)
  {
    return <Flex style={{width: "100%", height: "100vh"}}justify='center' align='center'><Loader /></Flex>
  }

  if (error) return <div>Error occurred while fetching workspaces</div>;

  return (
    <>
    <BrowserRouter>
      {isAuthenticated && <Navbar />}
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/profile" element={<ProtectedRoute><ProfilePage /></ProtectedRoute>} />
          <Route path="/workspaces/:workspaceId/settings" element={<ProtectedRoute><WorkspaceSettings /></ProtectedRoute>} />
          <Route path="/workspaces/:workspaceId" element={<ProtectedRoute><MainWindow /></ProtectedRoute>} />
        </Routes>
      </BrowserRouter>
    </>
  );
};