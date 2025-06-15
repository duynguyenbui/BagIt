import Link from "next/link";
import { cn } from "@/lib/utils";

interface LogoProps {
  size?: "sm" | "md" | "lg";
  description?: string;
  className?: string;
}

const sizeMap = {
  sm: ["h-8 w-8", "text-lg", "text-lg"],
  md: ["h-10 w-10", "text-lg", "text-xl"],
  lg: ["h-12 w-12", "text-xl", "text-2xl"],
};

export const Logo = ({ size = "md", description = "BagIt", className }: LogoProps) => {
  const [boxSize, letterSize, textSize] = sizeMap[size];

  return (
    <Link href="/" className={cn("flex items-center space-x-3", className)}>
      <div
        className={cn(
          "relative rounded-xl bg-gradient-to-br from-blue-500 via-blue-600 to-blue-700 flex items-center justify-center shadow-lg hover:shadow-xl transition-all duration-300",
          boxSize
        )}
      >
        <div className="absolute inset-0 rounded-xl bg-gradient-to-br from-blue-400/20 to-transparent" />
        <span className={cn("relative text-white font-bold drop-shadow-sm", letterSize)}>B</span>
      </div>
      {description && (
        <span className={cn("font-bold bg-gradient-to-r from-blue-600 to-blue-800 bg-clip-text text-transparent", textSize)}>
          {description}
        </span>
      )}
    </Link>
  );
};
