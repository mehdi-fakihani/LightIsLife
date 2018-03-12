#! /bin/sh

project="Light is life"
echo "Project path : " $TRAVIS_BUILD_DIR

echo "Attempting to build $project for Windows"
sudo /Applications/Unity/Unity.app/Contents/MacOS/Unity
  -batchmode 
  -nographics 
  -logFile $(pwd)/unity.log 
  -projectPath $TRAVIS_BUILD_DIR 
  -buildWindowsPlayer "$(pwd)/Build/windows/$project.exe"
  -stackTraceLogType Full

echo "Attempting to build $project for OS X"
sudo /Applications/Unity/Unity.app/Contents/MacOS/Unity 
  -batchmode 
  -nographics 
  -logFile $(pwd)/unity.log 
  -projectPath $TRAVIS_BUILD_DIR 
  -buildOSXUniversalPlayer "$(pwd)/Build/osx/$project.app"
  -stackTraceLogType Full

echo "Attempting to build $project for Linux"
sudo /Applications/Unity/Unity.app/Contents/MacOS/Unity 
  -batchmode 
  -nographics
  -logFile $(pwd)/unity.log 
  -projectPath $TRAVIS_BUILD_DIR 
  -buildLinuxUniversalPlayer "$(pwd)/Build/linux/$project.exe"
  -stackTraceLogType Full

echo 'Logs from build'
cat $(pwd)/unity.log


echo 'Attempting to zip builds'
zip -r $(pwd)/Build/linux.zip $(pwd)/Build/linux/
zip -r $(pwd)/Build/mac.zip $(pwd)/Build/osx/
zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/