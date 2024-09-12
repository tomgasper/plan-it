import {
    TextInput,
    PasswordInput,
    Checkbox,
    Anchor,
    Paper,
    Title,
    Text,
    Container,
    Group,
    Button,
  } from '@mantine/core';
  import classes from './Login.module.css';

  import { useState, FormEvent } from 'react';
  import { useLoginMutation } from '../../services/auth-api';
  import { setCredentials } from '../../redux/authSlice';
  import { useAppDispatch } from '../../hooks/reduxHooks';
  import { AuthResponse} from '../../types/Auth';
import { useNavigate } from 'react-router-dom';
import { notifications } from '@mantine/notifications';
  
  export function Login() {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [login, { isLoading }] = useLoginMutation();
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const userData: AuthResponse = await login({ email, password }).unwrap();
            dispatch(setCredentials({user: userData.user, token: userData.token}));

            notifications.show({
                title: 'Login successful',
                message: 'You have been successfully logged in',
                color: 'green'
            });
            navigate('/workspace');
        } catch (error) {
            const err = error as { data?: { title?: string; errors?: Record<string, string[]> } };

        // Display more personalized error message including server validation errors
        if (err.data?.title)
        {
            console.log(err.data)
            let errorMessage = err.data.title;
            if (err.data.errors)
            {
                for (const [_,value] of Object.entries(err.data.errors)) {
                    errorMessage += '\n' + value;
                }
            }

            notifications.show({
                title: 'Error logging in',
                message: errorMessage,
                color: 'red'
            });
            }
        }
    };
    
    return (
      <Container style={{minWidth:'20%'}} size={600} my={'5%'}>
        <Title ta="center" className={classes.title}>
          Welcome back!
        </Title>
        <Text c="dimmed" size="sm" ta="center" mt={5}>
          Do not have an account yet?{' '}
          <Anchor size="sm" component="button">
            Create account
          </Anchor>
        </Text>
  
        <Paper withBorder shadow="md" p={30} mt={30} radius="md">
          <TextInput label="Email" placeholder="you@email.com" value={email} onChange={(e) => setEmail(e.target.value) } required />
          <PasswordInput label="Password" placeholder="Your password" onChange={(e) => setPassword(e.target.value)} required mt="md" />
          <Group justify="space-between" mt="lg">
            <Checkbox label="Remember me" />
            <Anchor component="button" size="sm">
              Forgot password?
            </Anchor>
          </Group>
          <Button fullWidth mt="xl" onClick={ handleSubmit } loading={isLoading}>
            Sign in
          </Button>
        </Paper>
      </Container>
    );
  }