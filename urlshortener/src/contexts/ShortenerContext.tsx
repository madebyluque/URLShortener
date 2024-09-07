import { createContext, ReactNode, useState } from "react";
import { api } from "../lib/axios";
import { ShortenUrlSchema } from "../types/shortenLink.types";
import { AxiosError } from "axios";
import { FieldErrors } from "react-hook-form";
import { showToast } from "../utils/showErrorToast";

interface ShortenerContextType {
  shortenedUrl: string,
  isSubmitting: boolean,
  shortenUrl: (command: ShortenUrlSchema, formErrors: FieldErrors | null) => Promise<void>,
}

interface ShortenerProviderProps {
  children: ReactNode
}

export const ShortenerContext = createContext({} as ShortenerContextType);

export function ShortenerProvider({children}: ShortenerProviderProps) {
  const [shortenedUrl, setShortenedUrl] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false);

  const shortenUrl = async (command: ShortenUrlSchema, formErrors: FieldErrors | null = null) => {
    setShortenedUrl('')
    setIsSubmitting(true);
    if(formErrors?.root?.message) {
      showToast(formErrors.root?.message);
    }

    try {
      const response = await api.post('http://localhost:8080/shortener', command);
      setShortenedUrl(response.data.address);
    } catch (err) {
      const axiosError = err as AxiosError;  // Type the error as AxiosError
      if (axiosError.response && axiosError.response.data) {
        showToast(axiosError.message || 'An error occurred');
      } else {
        showToast('An error occurred');
      }
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <ShortenerContext.Provider value={{ shortenedUrl, shortenUrl, isSubmitting }}>
      {children}
    </ShortenerContext.Provider>
  )
}