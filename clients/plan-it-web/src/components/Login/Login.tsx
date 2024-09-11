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
  
  export function Login() {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [login, { isLoading }] = useLoginMutation();
    const dispatch = useAppDispatch();
    
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const userData: AuthResponse = await login({ email, password }).unwrap();
            dispatch(setCredentials({user: userData.user, token: userData.token}));
        } catch (err) {
            
            console.error(err);
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
          <TextInput label="Email" placeholder="you@mantine.dev" value={email} onChange={(e) => setEmail(e.target.value) } required />
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