#kubectl create configmap config-test-map --from-file ./configs/ -o yaml --dry-run | kubectl apply -f -
kubectl create configmap config-test-map --from-file ./configs/ -o yaml --dry-run | kubectl replace -f -