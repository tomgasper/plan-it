import React, { useState, useEffect } from 'react';
import {
  Container,
  Title,
  TextInput,
  PasswordInput,
  Button,
  Group,
  Box,
  Avatar,
  FileInput,
} from '@mantine/core';
import { useForm } from '@mantine/form';
import { useGetUserQuery, useUpdateUserMutation, useUploadAvatarMutation } from '../../services/planit-api';
import { showNotification } from '@mantine/notifications';
import { useAppSelector } from '../../hooks/reduxHooks';
import { useNavigate } from 'react-router-dom';

interface ProfileFormValues {
  firstName: string;
  lastName: string;
  password: string;
  confirmPassword: string;
}

export function ProfilePage() {
  const userFromToken = useAppSelector(state => state.auth.user);
  const { data: currentUser } = useGetUserQuery(userFromToken?.id ?? "");
  const [updateUser, { isLoading: isUpdating }] = useUpdateUserMutation();
  const [uploadAvatar, { isLoading: isUploading }] = useUploadAvatarMutation();
  const [avatarFile, setAvatarFile] = useState<File | null>(null);

  const form = useForm<ProfileFormValues>({
    initialValues: {
      firstName: currentUser?.firstName ?? '',
      lastName: currentUser?.lastName ?? '',
      password: '',
      confirmPassword: '',
    },
    validate: {
      firstName: (value : string) => (value.length < 2 ? 'First name must have at least 2 characters' : null),
      lastName: (value: string) => (value.length < 2 ? 'Last name must have at least 2 characters' : null),
      password: (value : string) => (
        value.length > 0 && value.length < 8
          ? 'Password must be at least 8 characters long'
          : null
      ),
      confirmPassword: (value: string, values: ProfileFormValues) => 
        value !== values.password ? 'Passwords do not match' : null,
    },
  });

  useEffect(() => {
    if (currentUser) {
      form.setValues({
        firstName: currentUser.firstName || '',
        lastName: currentUser.lastName || '',
      });
    }
  }, [currentUser, form]);

  const handleSubmit = async (values: ProfileFormValues) => {
    if (!currentUser) {
      showNotification({
        title: 'Error',
        message: 'User not found',
        color: 'red',
      });
      return;
    }

    try {
      await updateUser({
        userId: currentUser.id,
        firstName: values.firstName,
        lastName: values.lastName,
        ...(values.password ? { password: values.password } : {}),
      }).unwrap();

      if (avatarFile) {
        const formData = new FormData();
        formData.append('avatar', avatarFile);
        await uploadAvatar({ userId: currentUser.id, avatar: formData }).unwrap();
      }

      showNotification({
        title: 'Success',
        message: 'Profile updated successfully',
        color: 'green',
      });
    } catch (error) {
      showNotification({
        title: 'Error',
        message: 'Failed to update profile',
        color: 'red',
      });
    }
  };

  return (
    <Container size="sm">
      <Title order={2} mb="xl">Edit Profile</Title>
      <Box mb="xl">
        <Group justify="center">
          <Avatar 
            src={currentUser?.avatarUrl} 
            size={120} 
            radius={120} 
            alt="User avatar"
          />
        </Group>
        <Group justify="center" mt="md">
          <FileInput
            placeholder="Change avatar"
            accept="image/*"
            onChange={setAvatarFile}
          />
        </Group>
      </Box>
      <form onSubmit={form.onSubmit(handleSubmit)}>
        <TextInput
          label="First Name"
          placeholder="Your first name"
          {...form.getInputProps('firstName')}
          mb="md"
        />
        <TextInput
          label="Last Name"
          placeholder="Your last name"
          {...form.getInputProps('lastName')}
          mb="md"
        />
        <PasswordInput
          label="New Password"
          placeholder="Enter new password"
          {...form.getInputProps('password')}
          mb="md"
        />
        <PasswordInput
          label="Confirm New Password"
          placeholder="Confirm new password"
          {...form.getInputProps('confirmPassword')}
          mb="xl"
        />
        <Group justify="right">
          <Button type="submit" loading={isUpdating || isUploading}>
            Save Changes
          </Button>
        </Group>
      </form>
    </Container>
  );
}