# Shortest-Path-And-Rhumb-Line
This project simulates the generation of shortest paths and rhumb lines between coordinates. It displays the final generated paths on a 2D plane and a 3D sphere. The project is created using Unity3d and provides the following features:

| Setting the start and end coordinates | Displaying the paths on a 2D plane and a 3D sphere | 
| :-----: | :-------: |
| <img src="docs/SettingCoordinates.png" height="250" /> | <img src="docs/DisplayingPaths.gif" height="250" /> |
| Tutorial on how to compute the shortest path | Traversing the paths |
| <img src="docs/ShortestPathTutorial.gif" height="250" /> | <img src="docs/TraversingPaths.gif" height="250" /> |

# Demo

![Simulation](docs/Simulation.gif)

We created a WebGL application that demonstrates different features provided by our simulation.
A complete demo can be found on [https://omaddam.github.io/Shortest-Path-And-Rhumb-Line/](https://omaddam.github.io/Shortest-Path-And-Rhumb-Line/).

# Getting Started

These instructions will get you a copy of the project on your local machine for development and testing purposes.

### Prerequisites

The things you need to install before you proceed with development:

1) [Unity3d (2020.2.0f1)](https://unity3d.com/get-unity/download/archive) [required].

### Installing

A step by step guide to get you started with development.

#### Download, clone, and setup the repository

```git
git clone https://github.com/omaddam/Shortest-Path-And-Rhumb-Line.git
```

#### Initialize git flow

```git
git flow init
```

# Standards

### General Standards

* Line ending: CRLF
* Case styles: Camel, Pascal, and Snake case
  * Arguments, paramters, and local variables: camel case (e.g. shortestPath)
  * Global variables: pascal case (e.g. StartCoordinates)
  * Constants and static variables: snake case (ALL CAPS) (e.g. DEFAULT_COLOR)
* Methods naming convention:
  * Pascal case (e.g. GeneratePath)
  * Verbs

### Commenting Standards

* `///` Summaries: Full-usage of English grammar and punctuation. (e.g. Add periods to the end of your summaries, as if you were writing a phrase or sentence.)
*  `//` In-line comments: quick, point-form. Grammar and punctuation not needed

### Assets / App

* Contains all scripts and resources used in the demo.
* Scripts are created under Assets/App/Scripts folder.

### Third Party Packages

* All packages should be included under Assets/ThirdParty folder.
* Contains all packages downloaded from the Unity3d store.

### Assets / Others

* All components should be included under Assets/\<Name> folder. (e.g. Assets/SphericalPaths)
* Each component should be isolated and under **NO CIRCUMSTANCES** referencing or using another component's scripts.
* Components are **NOT** allowed to reference or call application/demo scripts.

# Code Based Documentation

## Assets / SphericalPaths

This folder contains the implementation of the algorithms that compute the shortest path and the constant path (rhumb line). The algorithms' implementation can be found in the *DataStructure* folder while the rest of the components are for visualization purposes.