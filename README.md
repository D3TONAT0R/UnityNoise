# UnityNoise

[![Unity](https://img.shields.io/badge/Unity-2019.4+-blue.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

## Description

A Unity package that contains a collection of functions for generating various kinds of noise both on the CPU and GPU.

### Available Noise Functions
|Type           |1D                |2D                |3D                |4D                |
|---------------|------------------|------------------|------------------|------------------|
|Perlin         |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|
|Simplex        |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:x:               |
|Voronoi        |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|
|Cellular       |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|

All noise generators also include functions for generating a fractal version.

## Installation

### Option 1: Unity Package Manager

Open the Package Manager window, click on "Add Package from Git URL ...", then enter the following:
```
https://github.com/d3tonat0r/unitynoise.git
```

### Option 2: Manually Editing packages.json

Add the following line to your project's `Packages/manifest.json`:

```json
"com.example.package": "https://github.com/d3tonat0r/unitynoise.git"
```

### Option 3: Manual Installation

You can also download this repository and extract the ZIP file anywhere inside your project's Assets folder.

## Usage

### In Scripts

All noise generators are found inside the ```UnityNoise``` namespace.

Example:
```csharp
using UnityNoise;

-----

Vector3 pos = new Vector3(0.5f, 1.0f, 2.0f);

//Simple 3D Perlin 
float noise = PerlinNoise.Instance.GetNoise3D(pos);

//Fractal 3D Perlin (parameters: octaves, lacunarity, persistence, scale)
FractalSettings fractal = new FractalSettings(4, 2f, 0.5f, 1);
float noise = PerlinNoise.Instance.GetNoise3D(pos, fractal);
```

### In Shaders

Add a reference to the Include file you want to use, e.g:
```
#include "Packages/com.github.d3tonat0r.unitynoise/Shaders/PerlinNoise.cginc"
```

Example:
```hlsl
//Simple 3D Perlin
float noise = GetPerlinNoise3D(position.xyz);

//Fractal 3D Perlin
FractalSettings settings;
settings.octaves = 4;
settings.persistence = 0.5;
settings.lacunarity = 2.0;
float noise = ComputePerlinNoise3D(position.xyz, settings);
```