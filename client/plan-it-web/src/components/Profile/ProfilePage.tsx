/* eslint-disable @typescript-eslint/no-unsafe-call */
/* eslint-disable @typescript-eslint/no-unsafe-member-access */

import { useState, useEffect } from 'react';
import {
  Title,
  TextInput,
  PasswordInput,
  Button,
  Group,
  Box,
  Avatar,
  FileInput,
  Flex,
} from '@mantine/core';
import { useForm } from '@mantine/form';
import { useGetUserQuery, useUpdateUserMutation, useUploadAvatarMutation } from '../../services/planit-api';
import { showNotification } from '@mantine/notifications';
import { useAppSelector } from '../../hooks/reduxHooks';

interface ProfileFormValues {
  firstName: string;
  lastName: string;
  oldPassword: string;
  newPassword: string;
}

export function ProfilePage() {
  const authState = useAppSelector(state => state.auth);
  const userFromToken = useAppSelector(state => state.auth.user);
  console.log(authState);
  const { data: currentUser, refetch: refetchUser } = useGetUserQuery(userFromToken?.id ?? "");
  const [updateUser, { isLoading: isUpdating }] = useUpdateUserMutation();
  const [uploadAvatar, { isLoading: isUploading }] = useUploadAvatarMutation();
  const [avatarFile, setAvatarFile] = useState<File | null>(null);

  const form = useForm<ProfileFormValues>({
    initialValues: {
      firstName: currentUser?.firstName ?? '',
      lastName: currentUser?.lastName ?? '',
      oldPassword: '',
      newPassword: '',
    },
    validate: {
      firstName: (value : string) => (value.length < 2 ? 'First name must have at least 2 characters' : null),
      lastName: (value: string) => (value.length < 2 ? 'Last name must have at least 2 characters' : null),
      oldPassword: (value : string) => (
        value.length > 0 && value.length < 8
          ? 'Password must be at least 8 characters long'
          : null
      ),
      newPassword: (value: string) => 
        value.length > 0 && value.length < 8
          ? 'Password must be at least 8 characters long'
          : null
    },
  });

  useEffect(() => {
    if (currentUser) {
      form.setValues({
        firstName: currentUser.firstName || '',
        lastName: currentUser.lastName || '',
      });
    }
  }, [currentUser]);

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
        ...(values.oldPassword ? { oldPassword: values.oldPassword } : {}),
        ...(values.newPassword ? { newPassword: values.newPassword } : {}),
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

      await refetchUser();
    } catch {
      showNotification({
        title: 'Error',
        message: 'Failed to update profile',
        color: 'red',
      });
    }
  };

  return (
    <Flex style={{width:"100%"}} justify='center' direction='column' align='center'>
      <Title order={3} mb="xl" mt={25} >Edit Profile</Title>
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
          label="Old Password"
          placeholder="Enter old password"
          {...form.getInputProps('oldPassword')}
          mb="md"
        />
        <PasswordInput
          label="New Password"
          placeholder="Confirm new password"
          {...form.getInputProps('newPassword')}
          mb="xl"
        />
        <Group justify="right">
          <Button type="submit" loading={isUpdating || isUploading}>
            Save Changes
          </Button>
        </Group>
      </form>
      </Flex>
  );
}