import {
    TextInput,
    PasswordInput,
    Checkbox,
    Paper,
    Title,
    Text,
    Container,
    Group,
    Button,
  } from '@mantine/core';
  import classes from './Register.module.css';
  import { useState, FormEvent } from 'react';
  import { useRegisterMutation } from '../../services/auth-api';
  import { setCredentials } from '../../redux/authSlice';
  import { useAppDispatch } from '../../hooks/reduxHooks';
  import { AuthResponse } from '../../types/Auth';
 import { notifications } from '@mantine/notifications';
import { Link, useNavigate } from 'react-router-dom';
  
  export function Register() {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [confirmPassword, setConfirmPassword] = useState<string>('');
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [register, { isLoading }] = useRegisterMutation();
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
  
    const handleSubmit = async (e: FormEvent) => {
      e.preventDefault();
      if (password !== confirmPassword) {

        notifications.show({
            title: 'Error registering new account',
            message: 'Passwords do not match',
            color: 'red'
        });
        return;
      }

      try {
        const userData: AuthResponse = await register({
            email,
            password,
            firstName,
            lastName
        }).unwrap();
        dispatch(setCredentials({user: userData.user, token: userData.token}));

        notifications.show({
            title: 'Account registered',
            message: 'Your account has been successfully registered',
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
                for (const [value] of Object.entries(err.data.errors)) {
                    errorMessage += '\n' + value;
                }
            }

            notifications.show({
                title: 'Error registering new account',
                message: errorMessage,
                color: 'red'
            });
            }
        }
    };
  
    return (
      <Container style={{width:'450px'}} size={600} my={'5%'}>
        <Title ta="center" className={classes.title}>
          Create an account
        </Title>
        <Text c="dimmed" size="sm" ta="center" mt={5}>
          Already have an account?{' '}
          <Link to="/login">
            Login
          </Link>
        </Text>
  
        <Paper withBorder shadow="md" p={30} mt={30} radius="md">
          <TextInput 
            label="First Name" 
            placeholder="Your first name" 
            value={firstName} 
            onChange={(e) => setFirstName(e.target.value)} 
            required 
          />
          <TextInput 
            label="Last Name" 
            placeholder="Your last name" 
            value={lastName} 
            onChange={(e) => setLastName(e.target.value)} 
            required
            mt="md"
          />
          <TextInput 
            label="Email" 
            placeholder="you@example.com" 
            value={email} 
            onChange={(e) => setEmail(e.target.value)} 
            required 
            mt="md"
          />
          <PasswordInput 
            label="Password" 
            placeholder="Your password" 
            value={password}
            onChange={(e) => setPassword(e.target.value)} 
            required 
            mt="md" 
          />
          <PasswordInput 
            label="Confirm Password" 
            placeholder="Confirm your password" 
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)} 
            required 
            mt="md" 
          />
          <Group justify="space-between" mt="lg">
            <Checkbox label="I agree to the terms and conditions" />
          </Group>
          <Button fullWidth mt="xl" onClick={handleSubmit} loading={isLoading}>
            Sign up
          </Button>
        </Paper>
      </Container>
    );
  }