﻿/* Copyright (c) Citrix Systems Inc. 
 * All rights reserved. 
 * 
 * Redistribution and use in source and binary forms, 
 * with or without modification, are permitted provided 
 * that the following conditions are met: 
 * 
 * *   Redistributions of source code must retain the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer. 
 * *   Redistributions in binary form must reproduce the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer in the documentation and/or other 
 *     materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
 * SUCH DAMAGE.
 */

using XenAdmin.Network;
using XenAPI;
using XenAdmin.Core;


namespace XenAdmin.Actions
{
    public class RunPolicyNowAction<T> : PureAsyncAction where T : XenObject<T>
    {
        private T _policy;
        public RunPolicyNowAction(T policy)
            : base(policy.Connection, string.Format(Messages.RUN_POLICY, policy.Name))
        {
            _policy = policy;
            Pool = Helpers.GetPool(policy.Connection);
        }

        protected override void Run()
        {
            if (typeof(T) == typeof(VMPP))
            {
                var _vmpp = _policy as VMPP;
                Result = VMPP.protect_now(Session, _vmpp.opaque_ref);
                Description = string.Format(Messages.RUN_POLICY_STARTED, _vmpp.Name);
            }
            else
            {
                var _vmss = _policy as VMSS;
                Result = VMSS.protect_now(Session, _vmss.opaque_ref);
                Description = string.Format(Messages.RUN_POLICY_STARTED, _vmss.Name);
            }
        }
    }
}
