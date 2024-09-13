import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAppSelector } from '../hooks/reduxHooks';
import { showNotification } from '@mantine/notifications';
import { Flex, Loader } from '@mantine/core';

interface ProtectedRouteProps {
  children: React.ReactNode;
}

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const { isAuthenticated, isLoading } = useAppSelector(state => state.auth);
  console.log(isLoading);
  const location = useLocation();

  if (isLoading) {
    return <Flex style={{width: "100%", height: "100vh"}} justify='center' align='center'><Loader /></Flex>;
  }

  if (!isAuthenticated) {
    showNotification({
      title: 'Error',
      message: 'You must be logged in to view this page',
      color: 'red',
    });
    // Redirect them to the /login page, but save the current location they were
    // trying to go to when they were redirected. This allows us to send them
    // along to that page after they login, which is a nicer user experience
    // than dropping them off on the home page.
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <>{children}</>;
};
