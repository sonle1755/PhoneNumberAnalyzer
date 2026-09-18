import { motion, type Variants } from "framer-motion";

export function InitialTransition() {
  const blackBox: Variants = {
    initial: {
      height: "100vh",
      bottom: 0,
    },
    animate: {
      height: 0,
      transition: {
        when: "afterChildren",
        duration: 1.5,
        ease: [0.87, 0, 0.13, 1],
      },
    },
  };

  const textContainer: Variants = {
    initial: {
      opacity: 1,
    },
    animate: {
      opacity: 0,
      transition: {
        duration: 0.25,
        when: "afterChildren",
      },
    },
  };

  const text: Variants = {
    initial: {
      y: 40,
    },
    animate: {
      y: 90,
      transition: {
        duration: 1.5,
        ease: [0.87, 0, 0.13, 1],
      },
    },
  };

  return (
    <motion.div
      initial="initial"
      animate="animate"
      variants={blackBox}
      onAnimationStart={() => (document.body.style.overflowY = "hidden")}
      onAnimationComplete={() => (document.body.style.overflowY = "auto")}

      style={{
        position: "fixed",
        width: "100%",
        backgroundColor: "black",
        zIndex: 50,
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <motion.svg
        variants={textContainer}
        style={{
          position: "absolute",
          zIndex: 50,
        }}
      >
        <pattern
          id="pattern"
          patternUnits="userSpaceOnUse"
          width={750}
          height={800}
        >
          <rect style={{ fill: "#ffffff", width: "100%", height: "100%" }} />
          <motion.rect
            variants={text}
            style={{
              fill: "#4b5563",
              width: "100%",
              height: "100%",
            }} // equivalent to Tailwind's gray-600
            initial={{ x: -750 }}
            animate={{ x: 0 }}
            transition={{
              duration: 1,
              repeat: Infinity,
              repeatType: "reverse",
            }}
          />
        </pattern>

        <text
          textAnchor="middle"
          dominantBaseline="middle"
          x="50%"
          y="50%"
          style={{
            fontSize: "2.25rem",
            fontWeight: 700,
            fill: "url(#pattern)",
          }}
        >
          SoiSim
        </text>
      </motion.svg>
    </motion.div>
  );
}
