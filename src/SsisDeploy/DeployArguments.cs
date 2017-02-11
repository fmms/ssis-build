﻿//-----------------------------------------------------------------------
//   Copyright 2017 Roman Tumaykin
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//-----------------------------------------------------------------------

namespace SsisDeploy
{
    public class DeployArguments : IDeployArguments
    {
        public DeployArguments()
        {
            EraseSensitiveInfo = false;
        }

        public string DeploymentFilePath { get; private set; }

        public string ServerInstance { get; private set; }

        public string Catalog { get; private set; }

        public string Folder { get; private set; }

        public string ProjectName { get; private set; }

        public bool EraseSensitiveInfo { get; private set; }

        public string ProjectPassword { get; private set; }

        public void ProcessArgs(string[] args)
        {
            var startPos = 0;

            if (!args[0].StartsWith("-"))
            {
                DeploymentFilePath = args[0];
                startPos++;
            }

            for (var argPos = startPos; argPos < args.Length; argPos++)
            {
                switch (args[argPos].ToLowerInvariant())
                {
                    case "-serverinstance":
                        ServerInstance = args[argPos++ + 1];
                        break;

                    case "-catalog":
                        Catalog = args[argPos++ + 1];
                        break;

                    case "-folder":
                        Folder = args[argPos++ + 1];
                        break;

                    case "-projectname":
                        ProjectName = args[argPos++ + 1];
                        break;

                    case "-erasesensitiveinfo":
                        EraseSensitiveInfo = true;
                        break;

                    case "-projectpassword":
                        ProjectPassword = args[argPos++ + 1];
                        break;

                    default:
                        throw new InvalidTokenException(args[argPos]);
                }
            }

            if (string.IsNullOrWhiteSpace(ServerInstance))
                throw new MissingRequiredArgumentException(nameof(ServerInstance));

            if (string.IsNullOrWhiteSpace(Catalog))
                throw new MissingRequiredArgumentException(nameof(Catalog));


            if (string.IsNullOrWhiteSpace(ServerInstance))
                throw new MissingRequiredArgumentException(nameof(Folder));

            if (string.IsNullOrWhiteSpace(Folder))
                throw new MissingRequiredArgumentException(nameof(Folder));
        }
    }
}