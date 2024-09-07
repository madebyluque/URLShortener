import { z } from "zod"

export const shortenUrlSchema = z.object({
  address: z.string().url().min(10).max(255),
})

export type ShortenUrlSchema = z.infer<typeof shortenUrlSchema>
