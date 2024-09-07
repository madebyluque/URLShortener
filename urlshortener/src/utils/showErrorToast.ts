import { createStandaloneToast } from "@chakra-ui/react"

export const showToast = (error: string) => {
  const { toast } = createStandaloneToast()

  toast({
    title: 'An error occurred.',
    description: error,
    position: 'top',
    status: 'error',
    duration: 5000,
    isClosable: true,
  })
}